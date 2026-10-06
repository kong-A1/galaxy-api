<?php

namespace App\OpenApi\CMS\Auth;

use App\OpenApi\Helpers\OpenApiResponse;
use App\Support\ApiError;
use OpenApi\Attributes as OA;

#[OA\Get(
    path: '/api/cms/auth/me',
    summary: 'Get authenticated user',
    description: 'Return the currently authenticated CMS user.',
    tags: ['CMS Auth'],

    responses: [
        new OpenApiResponse(
            code: 200,
            description: 'Authenticated user.',
            schema: '#/components/schemas/UserSchemasDoc',
            example: [
                'id' => '550e8400-e29b-41d4-a716-446655440000',
                'name' => 'John Doe',
                'email' => 'john.doe@example.com',
                'status' => 'active',
                'created_at' => '2026-01-01T00:00:00.000000Z',
                'updated_at' => '2026-01-01T00:00:00.000000Z',
            ],
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

class MeDoc {}
