<?php

namespace Tests\Feature\Controllers\CMS;

use App\Exceptions\AccountInactiveException;
use App\Exceptions\InvalidCredentialsException;
use App\Models\User;
use App\Services\CMS\AuthService;
use Illuminate\Foundation\Testing\RefreshDatabase;
use Mockery;
use Tests\TestCase;

class AuthControllerTest extends TestCase
{
    use RefreshDatabase;

    public function test_it_logs_in_successfully(): void
    {
        $user = User::factory()->create([
            'name' => 'John Doe',
            'email' => 'john.doe@example.com',
            'status' => 'active',
        ]);

        $authService = Mockery::mock(AuthService::class);

        $authService
            ->shouldReceive('login')
            ->once()
            ->with('john.doe@example.com', 'password123')
            ->andReturn($user);

        $this->app->instance(AuthService::class, $authService);

        $response = $this->postJson('/api/cms/auth/login', [
            'email' => 'john.doe@example.com',
            'password' => 'password123',
        ]);

        $response
            ->assertOk()
            ->assertJson([
                'data' => [
                    'id' => $user->id,
                    'name' => 'John Doe',
                    'email' => 'john.doe@example.com',
                    'status' => 'active',
                ],
            ]);
    }

    public function test_it_returns_invalid_credentials_when_credentials_are_invalid(): void
    {
        $authService = Mockery::mock(AuthService::class);

        $authService
            ->shouldReceive('login')
            ->once()
            ->with('john.doe@example.com', 'wrong-password')
            ->andThrow(new InvalidCredentialsException());

        $this->app->instance(AuthService::class, $authService);

        $response = $this->postJson('/api/cms/auth/login', [
            'email' => 'john.doe@example.com',
            'password' => 'wrong-password',
        ]);

        $response
            ->assertStatus(401)
            ->assertJson([
                'error' => [
                    'code' => 'INVALID_CREDENTIALS',
                    'message' => 'The provided credentials are incorrect.',
                    'details' => [],
                ],
            ]);
    }

    public function test_it_returns_account_inactive_when_user_is_inactive(): void
    {
        $authService = Mockery::mock(AuthService::class);

        $authService
            ->shouldReceive('login')
            ->once()
            ->with('john.doe@example.com', 'password123')
            ->andThrow(new AccountInactiveException());

        $this->app->instance(AuthService::class, $authService);

        $response = $this->postJson('/api/cms/auth/login', [
            'email' => 'john.doe@example.com',
            'password' => 'password123',
        ]);

        $response
            ->assertStatus(403)
            ->assertJson([
                'error' => [
                    'code' => 'ACCOUNT_INACTIVE',
                    'message' => 'The account is inactive.',
                    'details' => [],
                ],
            ]);
    }

    public function test_it_returns_validation_error_when_email_is_missing(): void
    {
        $response = $this->postJson('/api/cms/auth/login', [
            'password' => 'password123',
        ]);

        $response
            ->assertStatus(422)
            ->assertJson([
                'error' => [
                    'code' => 'VALIDATION_ERROR',
                ],
            ])
            ->assertJsonPath(
                'error.details.email',
                fn($errors) => is_array($errors) && count($errors) > 0,
            );
    }
}
