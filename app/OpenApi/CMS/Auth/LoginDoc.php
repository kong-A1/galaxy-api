<?php

namespace App\OpenApi\CMS\Auth;

use App\Support\ApiError;
use App\OpenApi\Helpers\ApiRequest;
use App\OpenApi\Helpers\ApiResponse;
use OpenApi\Attributes as OA;

#[OA\Post(
    path: '/api/cms/auth/login',
    summary: 'Login to CMS',
    description: 'Authenticate a CMS user using email and password.',
    tags: ['CMS Auth'],

    requestBody: new ApiRequest(
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
        new ApiResponse(
            code: 200,
            description: 'Login successful.',
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
            description: 'Invalid credentials.',
            schema: '#/components/schemas/Error',
            error: true,
            example: [
                'code' => ApiError::INVALID_CREDENTIALS['code'],
                'message' => ApiError::INVALID_CREDENTIALS['message'],
            ],
        ),

        new ApiResponse(
            code: 403,
            description: 'Account is inactive.',
            schema: '#/components/schemas/Error',
            error: true,
            example: [
                'code' => ApiError::ACCOUNT_INACTIVE['code'],
                'message' => ApiError::ACCOUNT_INACTIVE['message'],
                'details' => [],
            ],
        ),

        new ApiResponse(
            code: 422,
            description: 'Validation error.',
            schema: '#/components/schemas/Error',
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
