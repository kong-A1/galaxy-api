<?php

namespace App\OpenApi\Schemas\User;

use OpenApi\Attributes as OA;

#[OA\Schema(
    schema: 'UserSchemasDoc',
    type: 'object',
    required: [
        'id',
        'name',
        'email',
        'status',
        'created_at',
        'updated_at',
    ],
    properties: [
        new OA\Property(
            property: 'id',
            type: 'string',
            format: 'uuid',
            example: '550e8400-e29b-41d4-a716-446655440000',
        ),

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
            property: 'status',
            type: 'string',
            example: 'active',
        ),

        new OA\Property(
            property: 'created_at',
            type: 'string',
            format: 'date-time',
            example: '2026-01-01T00:00:00.000000Z',
        ),

        new OA\Property(
            property: 'updated_at',
            type: 'string',
            format: 'date-time',
            example: '2026-01-01T00:00:00.000000Z',
        ),
    ],
)]
class UserSchemasDoc {}
