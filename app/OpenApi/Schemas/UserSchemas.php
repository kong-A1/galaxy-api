<?php

namespace App\OpenApi\Schemas;

use OpenApi\Attributes as OA;

#[OA\Schema(
    schema: 'User',
    type: 'object',
    required: [
        'id',
        'name',
        'email',
        'status',
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
    ],
)]
class UserSchemas {}
