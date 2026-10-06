<?php

namespace App\Services\CMS;

use App\Exceptions\AccountInactiveException;
use App\Exceptions\InvalidCredentialsException;
use App\Models\User;
use App\Queries\CMS\UserQuery;
use Illuminate\Support\Facades\Auth;
use Illuminate\Support\Facades\Hash;

class AuthService
{
    public function __construct(private readonly UserQuery $userQuery) {}

    public function login(string $email, string $password): User
    {
        $user = $this->userQuery->findByEmail($email);

        if ($user === null) {
            throw new InvalidCredentialsException;
        }

        if (! Hash::check($password, $user->password)) {
            throw new InvalidCredentialsException;
        }

        if ($user->status !== 'active') {
            throw new AccountInactiveException;
        }

        Auth::login($user);

        return $user;
    }
}
