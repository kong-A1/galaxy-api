<?php

namespace Tests\Feature\CMS\User;

use App\Models\User;
use Illuminate\Foundation\Testing\RefreshDatabase;
use Tests\Concerns\UserTest;
use Tests\TestCase;

class CreateUserTest extends TestCase
{
    use RefreshDatabase, UserTest;

    public function test_it_returns_unauthenticated_when_user_is_not_authenticated(): void
    {
        $payload = [
            'name' => 'John Doe',
            'email' => 'john.doe@example.com',
            'password' => 'pass@123',
        ];

        $response = $this->postJson(
            '/api/cms/user/create',
            $payload,
        );

        $response
            ->assertStatus(401)
            ->assertJson([
                'error' => [
                    'code' => 'UNAUTHENTICATED',
                    'message' => 'Unauthenticated.',
                    'details' => [],
                ],
            ]);

        $this->assertDatabaseCount('users', 0);
    }

    public function test_it_creates_user_successfully(): void
    {
        $actor = $this->createUserTest();
        $this->actingAs($actor);

        $payload = [
            'name' => 'John Doe',
            'email' => 'john.doe@example.com',
            'password' => 'pass@123',
        ];

        $response = $this->postJson(
            '/api/cms/user/create',
            $payload,
        );

        $response
            ->assertCreated()
            ->assertJson([
                'data' => [
                    'name' => 'John Doe',
                    'email' => 'john.doe@example.com',
                    'status' => 'active',
                ],
            ]);

        $this->assertDatabaseHas('users', [
            'name' => 'John Doe',
            'email' => 'john.doe@example.com',
            'status' => 'active',
            'created_by' => $actor->id,
        ]);
    }

    public function test_it_returns_validation_error_when_email_is_missing(): void
    {
        $actor = $this->createUserTest();
        $this->actingAs($actor);

        $payload = [
            'name' => 'John Doe',
            'password' => 'pass@123',
        ];

        $response = $this->postJson(
            '/api/cms/user/create',
            $payload,
        );

        $response
            ->assertStatus(422)
            ->assertJson([
                'error' => [
                    'code' => 'VALIDATION_ERROR',
                    'message' => 'The given data is invalid.',
                    'details' => [
                        'email' => [
                            'Email is required',
                        ],
                    ],
                ],
            ]);

        $this->assertDatabaseCount('users', 1);
    }

    public function test_it_returns_conflict_when_email_already_exists(): void
    {
        $actor = $this->createUserTest();
        $this->actingAs($actor);

        User::factory()->create([
            'name' => 'Existing User',
            'email' => 'john.doe@example.com',
        ]);

        $payload = [
            'name' => 'John Doe',
            'email' => 'john.doe@example.com',
            'password' => 'pass@123',
        ];

        $response = $this->postJson(
            '/api/cms/user/create',
            $payload,
        );

        $response
            ->assertStatus(409)
            ->assertJson([
                'error' => [
                    'code' => 'EMAIL_ALREADY_EXISTS',
                    'message' => 'The email has already been taken.',
                    'details' => [],
                ],
            ]);

        $this->assertDatabaseCount('users', 2);
    }
}
