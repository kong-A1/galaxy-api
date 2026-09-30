<?php

namespace App\Services\CMS;

use App\Models\User;
use App\Queries\CMS\UserQuery;
use Illuminate\Support\Facades\Auth;
use App\Exceptions\EmailAlreadyExistsException;

class UserService
{
    public function __construct(private readonly UserQuery $userQuery) {}

    public function create(array $data): User
    {
        if ($this->userQuery->existsByEmail($data['email'])) {
            throw new EmailAlreadyExistsException();
        }

        $data['status'] = 'active';
        $data['created_by'] = Auth::id();

        return $this->userQuery->create($data);
    }
}
