<?php

namespace Tests\Concerns;

use App\Models\User;

trait UserTest
{
    protected function createUserTest(): User
    {
        return User::factory()->create();
    }
}
