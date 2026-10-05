<?php

namespace App\OpenApi\CMS\Auth;

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
                'code' => 'UNAUTHENTICATED',
                'message' => 'The provided credentials are incorrect.',
                'details' => [],
            ],
        ),

        new ApiResponse(
            code: 403,
            description: 'Account is inactive.',
            schema: '#/components/schemas/Error',
            error: true,
            example: [
                'code' => 'ACCOUNT_INACTIVE',
                'message' => 'The account is inactive.',
                'details' => [],
            ],
        ),

        new ApiResponse(
            code: 422,
            description: 'Validation error.',
            schema: '#/components/schemas/Error',
            error: true,
            example: [
                'code' => 'VALIDATION_ERROR',
                'message' => 'The given data is invalid.',
                'details' => [
                    'email' => [
                        'The email field is required.',
                    ],
                ],
            ],
        ),
    ],
)]

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
                'code' => 'UNAUTHENTICATED',
                'message' => 'Unauthenticated.',
                'details' => [],
            ],
        ),
    ],
)]

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
            schema: '#/components/schemas/Error',
            error: true,
            example: [
                'code' => 'UNAUTHENTICATED',
                'message' => 'Unauthenticated.',
                'details' => [],
            ],
        ),
    ],
)]
class Auth {}
