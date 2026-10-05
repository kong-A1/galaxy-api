<?php

namespace Tests\Concerns;

use App\Models\User;

trait UserTest
{
    protected function createTestUser(): User
    {
        return User::factory()->create();
    }
}
