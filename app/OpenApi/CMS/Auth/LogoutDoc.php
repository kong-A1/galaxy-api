<?php

namespace App\OpenApi\CMS\Auth;

use App\OpenApi\Helpers\OpenApiResponse;
use App\Support\ApiError;
use OpenApi\Attributes as OA;

#[OA\Post(
    path: '/api/cms/auth/logout',
    summary: 'Logout from CMS',
    description: 'Logout the currently authenticated CMS user.',
    tags: ['CMS Auth'],

    responses: [
        new OpenApiResponse(
            code: 204,
            description: 'No Content.',
        ),

        new OpenApiResponse(
            code: 401,
            description: 'Unauthenticated.',
            schema: '#/components/schemas/OpenApiError',
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
