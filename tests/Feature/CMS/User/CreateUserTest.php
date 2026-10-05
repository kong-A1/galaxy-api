<?php

namespace Tests\Feature\CMS\User;

use App\Models\User;
use Illuminate\Foundation\Testing\RefreshDatabase;
use Tests\TestCase;

class CreateUserTest extends TestCase
{
    use RefreshDatabase;

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
                            'Email is required',
                        ],
                    ],
                ],
            ]);

        $this->assertDatabaseCount('users', 0);
    }

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
