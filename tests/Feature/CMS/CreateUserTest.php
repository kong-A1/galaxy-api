<?php

namespace Tests\Feature\CMS;

use App\Models\User;
use Illuminate\Foundation\Testing\RefreshDatabase;
use Tests\TestCase;

class CreateUserTest extends TestCase
{
    use RefreshDatabase;

    private const URL = '/api/cms/user/create';

    private array $payload = [
        'name' => 'John Doe',
        'email' => 'john.doe@example.com',
        'password' => 'password123',
    ];

    public function test_creates_user_from_form_data(): void
    {
        $this->post(self::URL, $this->payload, ['Accept' => 'application/json'])
            ->assertCreated()
            ->assertJsonPath('data.email', 'john.doe@example.com')
            ->assertJsonPath('data.status', 'active')
            ->assertJsonMissingPath('data.password');

        $this->assertDatabaseCount('users', 1);
    }

    public function test_creates_user_from_json(): void
    {
        $this->postJson(self::URL, $this->payload)
            ->assertCreated()
            ->assertJsonPath('data.name', 'John Doe')
            ->assertJsonPath('data.status', 'active');
    }

    public function test_rejects_duplicate_email(): void
    {
        User::factory()->create(['email' => $this->payload['email']]);

        $this->postJson(self::URL, $this->payload)
            ->assertStatus(409)
            ->assertJsonPath('error.code', 'EMAIL_ALREADY_EXISTS');
    }

    public function test_returns_validation_error_format(): void
    {
        $this->postJson(self::URL, ['name' => 'John'])
            ->assertStatus(422)
            ->assertJsonPath('error.code', 'VALIDATION_ERROR')
            ->assertJsonStructure(['error' => ['details' => ['email', 'password']]]);
    }
}
