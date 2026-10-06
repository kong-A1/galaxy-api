<?php

namespace App\OpenApi\CMS\User;

use App\OpenApi\Helpers\ApiRequest;
use App\OpenApi\Helpers\ApiResponse;
use App\Support\ApiError;
use OpenApi\Attributes as OA;

#[OA\Post(
    path: '/api/cms/user/create',
    summary: 'Create a new user',
    description: 'Create a new user account for the CMS.',
    tags: ['CMS Users'],

    requestBody: new ApiRequest(
        required: [
            'name',
            'email',
            'password',
        ],

        properties: [
            new OA\Property(
                property: 'name',
                type: 'string',
                example: 'John Doe',
            ),

            new OA\Property(
                property: 'email',
                type: 'string',
                format: 'email',
                example: 'john.doe@example.com',
            ),

            new OA\Property(
                property: 'password',
                type: 'string',
                minLength: 8,
                example: 'password123',
            ),
        ],
    ),

    responses: [
        new ApiResponse(
            code: 201,
            description: 'User created successfully.',
            schema: '#/components/schemas/UserSchemasDoc',
            example: [
                'id' => '550e8400-e29b-41d4-a716-446655440000',
                'name' => 'John Doe',
                'email' => 'john.doe@example.com',
                'status' => 'active',
            ],
        ),

        new ApiResponse(
            code: 409,
            description: 'Email already exists.',
            schema: '#/components/Error',
            error: true,
            example: [
                'code' => ApiError::EMAIL_ALREADY_EXISTS['code'],
                'message' => ApiError::EMAIL_ALREADY_EXISTS['message'],
            ],
        ),

        new ApiResponse(
            code: 422,
            description: 'Validation error.',
            schema: '#/components/Error',
            error: true,
            example: [
                'code' => ApiError::VALIDATION_ERROR['code'],
                'message' => ApiError::VALIDATION_ERROR['message'],
                'details' => [
                    'email' => [
                        'Email is required.',
                    ],
                ],
            ],
        ),
    ],
)]
class CreateUserDoc {}
