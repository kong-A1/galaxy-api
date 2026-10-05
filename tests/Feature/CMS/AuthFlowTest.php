<?php

namespace Tests\Feature\CMS;

use App\Models\User;
use Illuminate\Foundation\Testing\RefreshDatabase;
use Illuminate\Support\Facades\Auth;
use Tests\TestCase;

class AuthFlowTest extends TestCase
{
    use RefreshDatabase;

    public function test_it_completes_authentication_flow(): void
    {
        $user = User::factory()->create([
            'name' => 'John Doe',
            'email' => 'john.doe@example.com',
            'password' => 'password123',
            'status' => 'active',
        ]);

        // 1. Login
        $this
            ->withHeader('Origin', 'http://localhost:5173')
            ->postJson('/api/cms/auth/login', [
                'email' => 'john.doe@example.com',
                'password' => 'password123',
            ])
            ->assertOk()
            ->assertJson([
                'data' => [
                    'id' => $user->id,
                    'name' => 'John Doe',
                    'email' => 'john.doe@example.com',
                    'status' => 'active',
                ],
            ]);

        // 2. Get authenticated user
        $this
            ->withHeader('Origin', 'http://localhost:5173')
            ->getJson('/api/cms/auth/me')
            ->assertOk()
            ->assertJson([
                'data' => [
                    'id' => $user->id,
                    'name' => 'John Doe',
                    'email' => 'john.doe@example.com',
                    'status' => 'active',
                ],
            ]);

        // 3. Logout
        $this
            ->withHeader('Origin', 'http://localhost:5173')
            ->postJson('/api/cms/auth/logout')
            ->assertNoContent();

        $this->assertGuest('web');

        // Reset cached authentication guards.
        // This simulates a new HTTP request in the test environment.
        Auth::forgetGuards();

        // 4. Verify user is no longer authenticated
        $this
            ->withHeader('Origin', 'http://localhost:5173')
            ->getJson('/api/cms/auth/me')
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
