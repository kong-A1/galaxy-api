<?php

namespace App\Http\Response\CMS;

use App\Models\User;
use Illuminate\Http\JsonResponse;

class UserResponse
{
    public static function make(User $user, int $status = 200): JsonResponse
    {
        return response()->json([
            'data' => [
                'id' => $user->id,
                'name' => $user->name,
                'email' => $user->email,
                'status' => $user->status,
                'created_at' => $user->created_at,
                'updated_at' => $user->updated_at,
            ],
        ], $status);
    }
}
