<?php

namespace App\Services\CMS;

use App\Exceptions\EmailAlreadyExistsException;
use App\Models\User;
use App\Queries\CMS\UserQuery;
use Illuminate\Database\QueryException;
use Illuminate\Support\Facades\Auth;

class UserService
{
    public function __construct(private readonly UserQuery $userQuery) {}

    public function create(array $data): User
    {
        if ($this->userQuery->existsByEmail($data['email'])) {
            throw new EmailAlreadyExistsException;
        }

        $data['status'] = 'active';
        $data['created_by'] = Auth::id();

        try {
            return $this->userQuery->create($data);
        } catch (QueryException $e) {
            if (
                (string) $e->getCode() === '23505'
                && str_contains(
                    $e->getMessage(),
                    'users_email_unique',
                )
            ) {
                throw new EmailAlreadyExistsException;
            }

            throw $e;
        }
    }
}
