<?php

namespace Tests\Feature\CMS\User;

use App\Models\User;
use Illuminate\Foundation\Testing\RefreshDatabase;
use Tests\TestCase;

class CreateUserTest extends TestCase
{
    use RefreshDatabase;
    /**
     * Test create user successfully
     */
    public function test_it_creates_user_successfully(): void
    {
        $payload = [
            'name' => 'John Doe',
            'email' => 'john.doe@example.com',
            'password' => 'password123',
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
        ]);
    }

    /**
     * Test create user failed when email is missing
     */
    public function test_it_returns_validation_error_when_email_is_missing(): void
    {
        $payload = [
            'name' => 'John Doe',
            'password' => 'password123',
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
                            'The email field is required.',
                        ],
                    ],
                ],
            ]);

        $this->assertDatabaseCount('users', 0);
    }

    /**
     * Test create user failed when email already exists
     */
    public function test_it_returns_conflict_when_email_already_exists(): void
    {
        User::factory()->create([
            'name' => 'Existing User',
            'email' => 'john.doe@example.com',
        ]);

        $payload = [
            'name' => 'John Doe',
            'email' => 'john.doe@example.com',
            'password' => 'password123',
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

        $this->assertDatabaseCount('users', 1);
    }
}
