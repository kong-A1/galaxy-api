<?php

namespace App\Http\Controllers\CMS;

use App\Http\Controllers\Controller;
use App\Http\Requests\CMS\CreateUserRequest;
use App\Http\Response\CMS\UserResponse;
use App\Services\CMS\UserService;
use Illuminate\Http\JsonResponse;

class UserController extends Controller
{
    public function __construct(private readonly UserService $userService) {}

    public function create(CreateUserRequest $request): JsonResponse
    {
        $user = $this->userService->create($request->validated());

        return UserResponse::make($user, 201);
    }
}
