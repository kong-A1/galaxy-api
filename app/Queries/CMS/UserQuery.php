<?php

namespace App\Queries\CMS;

use App\Models\User;

class UserQuery
{
    /**
     * Check if a user exists by email.
     */
    public function existsByEmail(string $email): bool
    {
        return User::query()
            ->where('email', $email)
            ->exists();
    }

    /**
     * Create a new user.
     */
    public function create(array $data): User
    {
        return User::create($data);
    }
}
