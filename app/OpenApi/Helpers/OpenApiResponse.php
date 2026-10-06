<?php

namespace App\OpenApi\Helpers;

use DateTimeInterface;
use OpenApi\Attributes as OA;

class OpenApiResponse extends OA\Response
{
    public function __construct(
        int $code,
        string $description,
        ?string $schema = null,
        ?array $example = null,
        bool $error = false,
    ) {
        if ($schema === null && $example === null) {
            parent::__construct(
                response: $code,
                description: $description,
            );

            return;
        }

        $key = $error ? 'error' : 'data';

        if ($example !== null) {
            array_walk_recursive($example, function (&$value): void {
                if ($value instanceof DateTimeInterface) {
                    $value = $value->format('Y-m-d H:i:s.v');
                }
            });

            if ($error) {
                $example['details'] ??= [];
                $example['details'] = (object) $example['details'];
            }

            $example = [$key => $example];
        }

        parent::__construct(
            response: $code,
            description: $description,
            content: new OA\JsonContent(
                properties: [
                    new OA\Property(
                        property: $key,
                        ref: $schema,
                    ),
                ],
                example: $example,
            ),
        );
    }
}
