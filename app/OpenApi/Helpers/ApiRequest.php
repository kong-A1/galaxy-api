<?php

namespace App\OpenApi\Helpers;

use OpenApi\Attributes as OA;

class ApiRequest extends OA\RequestBody
{
    public function __construct(
        array $required = [],
        array $properties = [],
    ) {
        parent::__construct(
            required: true,
            content: [
                new OA\MediaType(
                    mediaType: 'application/x-www-form-urlencoded',
                    schema: new OA\Schema(
                        required: $required,
                        properties: $properties,
                    ),
                ),
                new OA\JsonContent(
                    required: $required,
                    properties: $properties,
                ),
            ],
        );
    }
}
