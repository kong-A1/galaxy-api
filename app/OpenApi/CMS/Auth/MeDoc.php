<?php

namespace App\OpenApi\CMS\Auth;

use App\Support\ApiError;
use App\OpenApi\Helpers\ApiResponse;
use OpenApi\Attributes as OA;

#[OA\Get(
    path: '/api/cms/auth/me',
    summary: 'Get authenticated user',
    description: 'Return the currently authenticated CMS user.',
    tags: ['CMS Auth'],

    responses: [
        new ApiResponse(
            code: 200,
            description: 'Authenticated user.',
            schema: '#/components/schemas/User',
            example: [
                'id' => '550e8400-e29b-41d4-a716-446655440000',
                'name' => 'John Doe',
                'email' => 'john.doe@example.com',
                'status' => 'active',
            ],
        ),

        new ApiResponse(
            code: 401,
            description: 'Unauthenticated.',
            schema: '#/components/schemas/Error',
            error: true,
            example: [
                'code' => ApiError::UNAUTHENTICATED['code'],
                'message' => ApiError::UNAUTHENTICATED['message'],
                'details' => [],
            ],
        ),
    ],
)]

class MeDoc {}
