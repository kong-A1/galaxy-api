<?php

namespace Tests\Feature\Controllers\CMS;

use App\Models\User;
use Illuminate\Foundation\Testing\RefreshDatabase;
use Tests\TestCase;

class AuthMeControllerTest extends TestCase
{
    use RefreshDatabase;

    public function test_it_returns_authenticated_user(): void
    {
        $user = User::factory()->create([
            'name' => 'John Doe',
            'email' => 'john.doe@example.com',
            'status' => 'active',
        ]);

        $this->actingAs($user);

        $response = $this->getJson('/api/cms/auth/me');

        $response
            ->assertOk()
            ->assertJson([
                'data' => [
                    'id' => $user->id,
                    'name' => 'John Doe',
                    'email' => 'john.doe@example.com',
                    'status' => 'active',
                ],
            ])
            ->assertExactJsonStructure([
                'data' => [
                    'id',
                    'name',
                    'email',
                    'status',
                    'created_at',
                    'updated_at',
                ],
            ])
            ->assertJsonMissingPaths([
                'data.password',
                'data.remember_token',
                'data.created_by',
                'data.updated_by',
                'data.deleted_by',
                'data.deleted_at',
                'data.email_verified_at',
            ]);
    }

    public function test_it_returns_unauthenticated_when_user_is_not_authenticated(): void
    {
        $response = $this->getJson('/api/cms/auth/me');

        $response
            ->assertStatus(401)
            ->assertJson([
                'error' => [
                    'code' => 'UNAUTHENTICATED',
                    'message' => 'Unauthenticated.',
                    'details' => [],
                ],
            ]);
    }
}
