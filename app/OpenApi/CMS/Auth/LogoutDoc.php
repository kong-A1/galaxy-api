<?php

namespace App\OpenApi\CMS\Auth;

use App\OpenApi\Helpers\ApiResponse;
use App\Support\ApiError;
use OpenApi\Attributes as OA;

#[OA\Post(
    path: '/api/cms/auth/logout',
    summary: 'Logout from CMS',
    description: 'Logout the currently authenticated CMS user.',
    tags: ['CMS Auth'],

    responses: [
        new ApiResponse(
            code: 204,
            description: 'Logout successful.',
        ),

        new ApiResponse(
            code: 401,
            description: 'Unauthenticated.',
            schema: '#/components/Error',
            error: true,
            example: [
                'code' => ApiError::UNAUTHENTICATED['code'],
                'message' => ApiError::UNAUTHENTICATED['message'],
                'details' => [],
            ],
        ),
    ],
)]

class LogoutDoc {}
