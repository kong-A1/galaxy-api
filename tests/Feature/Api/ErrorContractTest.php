<?php

namespace Tests\Feature\Api;

use App\Exceptions\InternalException;
use App\Exceptions\TooManyRequestsException;
use App\Models\User;
use App\Support\ApiError;
use Illuminate\Support\Facades\Route;
use Tests\Concerns\UserTest;
use Tests\TestCase;

class ErrorContractTest extends TestCase
{
    use UserTest;

    public function test_unauthenticated_response_follows_error_contract(): void
    {
        $response = $this->getJson('/api/cms/auth/me');

        $response
            ->assertStatus(401)
            ->assertJsonStructure([
                'error' => [
                    'code',
                    'message',
                    'details',
                ],
            ])
            ->assertJsonPath('error.code', ApiError::UNAUTHENTICATED['code'])
            ->assertJsonPath('error.message', ApiError::UNAUTHENTICATED['message'])
            ->assertJsonPath('error.details', []);
    }

    public function test_invalid_credentials_response_follows_error_contract(): void
    {
        $user = User::factory()->create([
            'email' => 'test@example.com',
            'password' => 'password123',
        ]);

        $response = $this->postJson('/api/cms/auth/login', [
            'email' => $user->email,
            'password' => 'wrong-password',
        ]);

        $response
            ->assertStatus(401)
            ->assertJsonStructure([
                'error' => [
                    'code',
                    'message',
                    'details',
                ],
            ])
            ->assertJsonPath('error.code', ApiError::INVALID_CREDENTIALS['code'])
            ->assertJsonPath('error.message', ApiError::INVALID_CREDENTIALS['message'])
            ->assertJsonPath('error.details', []);
    }

    public function test_inactive_account_response_follows_error_contract(): void
    {
        $user = User::factory()->create([
            'email' => 'inactive@example.com',
            'password' => 'password123',
            'status' => 'inactive',
        ]);

        $response = $this->postJson('/api/cms/auth/login', [
            'email' => $user->email,
            'password' => 'password123',
        ]);

        $response
            ->assertStatus(403)
            ->assertJsonStructure([
                'error' => [
                    'code',
                    'message',
                    'details',
                ],
            ])
            ->assertJsonPath('error.code', ApiError::ACCOUNT_INACTIVE['code'])
            ->assertJsonPath('error.message', ApiError::ACCOUNT_INACTIVE['message'])
            ->assertJsonPath('error.details', []);
    }

    public function test_duplicate_email_response_follows_error_contract(): void
    {
        $actor = $this->createUserTest();
        $this->actingAs($actor);

        $existingUser = User::factory()->create([
            'email' => 'existing@example.com',
        ]);

        $response = $this->postJson('/api/cms/user/create', [
            'name' => 'Another User',
            'email' => $existingUser->email,
            'password' => 'password123',
        ]);

        $response
            ->assertStatus(409)
            ->assertJsonStructure([
                'error' => [
                    'code',
                    'message',
                    'details',
                ],
            ])
            ->assertJsonPath('error.code', ApiError::EMAIL_ALREADY_EXISTS['code'])
            ->assertJsonPath('error.message', ApiError::EMAIL_ALREADY_EXISTS['message'])
            ->assertJsonPath('error.details', []);
    }

    public function test_validation_error_response_follows_error_contract(): void
    {
        $actor = $this->createUserTest();
        $this->actingAs($actor);

        $response = $this->postJson('/api/cms/user/create', [
            'name' => 'Test User',
            'password' => 'password123',
        ]);

        $response
            ->assertStatus(422)
            ->assertJsonStructure([
                'error' => [
                    'code',
                    'message',
                    'details',
                ],
            ])
            ->assertJsonPath('error.code', ApiError::VALIDATION_ERROR['code'])
            ->assertJsonPath('error.message', ApiError::VALIDATION_ERROR['message']);

        $this->assertArrayHasKey(
            'email',
            $response->json('error.details'),
        );
    }

    public function test_not_found_response_follows_error_contract(): void
    {
        $response = $this->getJson('/api/cms/does-not-exist');

        $response
            ->assertStatus(404)
            ->assertJsonStructure([
                'error' => [
                    'code',
                    'message',
                    'details',
                ],
            ])
            ->assertJsonPath('error.code', ApiError::NOT_FOUND['code'])
            ->assertJsonPath('error.message', ApiError::NOT_FOUND['message'])
            ->assertJsonPath('error.details', []);
    }

    public function test_method_not_allowed_response_follows_error_contract(): void
    {
        $response = $this->getJson('/api/cms/user/create');

        $response
            ->assertStatus(405)
            ->assertJsonStructure([
                'error' => [
                    'code',
                    'message',
                    'details',
                ],
            ])
            ->assertJsonPath('error.code', ApiError::METHOD_NOT_ALLOWED['code'])
            ->assertJsonPath('error.message', ApiError::METHOD_NOT_ALLOWED['message'])
            ->assertJsonPath('error.details', []);
    }

    public function test_too_many_requests_response_follows_error_contract(): void
    {
        Route::get('/api/test-too-many-requests', function () {
            throw new TooManyRequestsException;
        });

        $response = $this->getJson('/api/test-too-many-requests');

        $response
            ->assertStatus(429)
            ->assertJsonStructure([
                'error' => [
                    'code',
                    'message',
                    'details',
                ],
            ])
            ->assertJsonPath('error.code', ApiError::TOO_MANY_REQUESTS['code'])
            ->assertJsonPath('error.message', ApiError::TOO_MANY_REQUESTS['message'])
            ->assertJsonPath('error.details', []);
    }

    public function test_internal_server_error_response_follows_error_contract(): void
    {
        Route::get('/api/test-internal-error', function () {
            throw new InternalException;
        });

        $response = $this->getJson('/api/test-internal-error');

        $response
            ->assertStatus(500)
            ->assertJsonStructure([
                'error' => [
                    'code',
                    'message',
                    'details',
                ],
            ])
            ->assertJsonPath('error.code', ApiError::INTERNAL_SERVER_ERROR['code'])
            ->assertJsonPath('error.message', ApiError::INTERNAL_SERVER_ERROR['message'])
            ->assertJsonPath('error.details', []);
    }
}
