<?php

namespace Tests\Feature\Controllers\CMS;

use App\Models\User;
use Illuminate\Foundation\Testing\RefreshDatabase;
use Tests\TestCase;

class AuthLogoutControllerTest extends TestCase
{
    use RefreshDatabase;

    public function test_it_logs_out_authenticated_user(): void
    {
        $user = User::factory()->create([
            'status' => 'active',
        ]);

        $this->actingAs($user, 'web');

        $response = $this->withHeader('Origin', 'http://localhost:5173')->postJson('/api/cms/auth/logout');

        $response->assertNoContent();

        $this->assertGuest('web');
    }

    public function test_it_returns_unauthenticated_when_user_is_not_authenticated(): void
    {
        $response = $this->postJson('/api/cms/auth/logout');

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
