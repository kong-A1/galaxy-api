<?php

namespace Tests\Feature\Queries\CMS;

use App\Models\User;
use App\Queries\CMS\UserQuery;
use Illuminate\Foundation\Testing\RefreshDatabase;
use Tests\TestCase;

class UserQueryTest extends TestCase
{
    use RefreshDatabase;

    /**
     * Test find user by email successfully
     */
    public function test_it_finds_user_by_email(): void
    {
        $user = User::factory()->create([
            'email' => 'john.doe@example.com',
        ]);

        $query = new UserQuery;

        $result = $query->findByEmail('john.doe@example.com');

        $this->assertNotNull($result);
        $this->assertTrue($result->is($user));
    }

    /**
     * Test find user by email failed when email does not exist
     */
    public function test_it_returns_null_when_email_does_not_exist(): void
    {
        $query = new UserQuery;

        $result = $query->findByEmail('not-found@example.com');

        $this->assertNull($result);
    }
}
