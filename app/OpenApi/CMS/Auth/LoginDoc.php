<?php

namespace App\OpenApi\CMS\Auth;

use App\OpenApi\Helpers\OpenApiRequest;
use App\OpenApi\Helpers\OpenApiResponse;
use App\Support\ApiError;
use OpenApi\Attributes as OA;

#[OA\Post(
    path: '/api/cms/auth/login',
    summary: 'Login to CMS',
    description: 'Authenticate a CMS user using email and password.',
    tags: ['CMS Auth'],

    requestBody: new OpenApiRequest(
        required: [
            'email',
            'password',
        ],

        properties: [
            new OA\Property(
                property: 'email',
                type: 'string',
                format: 'email',
                example: 'john.doe@example.com',
            ),

            new OA\Property(
                property: 'password',
                type: 'string',
                example: 'password123',
            ),
        ],
    ),

    responses: [
        new OpenApiResponse(
            code: 200,
            description: 'Login successful.',
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
            description: 'Invalid credentials.',
            schema: '#/components/schemas/OpenApiError',
            error: true,
            example: [
                'code' => ApiError::INVALID_CREDENTIALS['code'],
                'message' => ApiError::INVALID_CREDENTIALS['message'],
            ],
        ),

        new OpenApiResponse(
            code: 403,
            description: 'Account is inactive.',
            schema: '#/components/schemas/OpenApiError',
            error: true,
            example: [
                'code' => ApiError::ACCOUNT_INACTIVE['code'],
                'message' => ApiError::ACCOUNT_INACTIVE['message'],
                'details' => [],
            ],
        ),

        new OpenApiResponse(
            code: 422,
            description: 'Validation error.',
            schema: '#/components/schemas/OpenApiError',
            error: true,
            example: [
                'code' => ApiError::VALIDATION_ERROR['code'],
                'message' => ApiError::VALIDATION_ERROR['message'],
                'details' => [
                    'email' => [
                        'The email field is required.',
                    ],
                ],
            ],
        ),
    ],
)]

class LoginDoc {}
