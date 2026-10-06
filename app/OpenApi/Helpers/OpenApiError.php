<?php

namespace App\OpenApi\Helpers;

use OpenApi\Attributes as OA;

#[OA\Schema(
    schema: 'OpenApiError',
    type: 'object',
    required: [
        'code',
        'message',
        'details',
    ],
    properties: [
        new OA\Property(
            property: 'code',
            type: 'string',
            example: 'EMAIL_ALREADY_EXISTS',
        ),

        new OA\Property(
            property: 'message',
            type: 'string',
            example: 'The email has already been taken.',
        ),

        new OA\Property(
            property: 'details',
            type: 'object',
            additionalProperties: new OA\AdditionalProperties(
                type: 'array',
                items: new OA\Items(
                    type: 'string',
                ),
            ),
            example: [],
        ),
    ],
)]
class OpenApiError {}
