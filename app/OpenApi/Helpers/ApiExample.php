<?php

namespace App\OpenApi\Helpers;

class ApiExample
{
    public static function data(array $data): array
    {
        return [
            'data' => $data,
        ];
    }

    public static function error(
        string $code,
        string $message,
        array $details = [],
    ): array {
        return [
            'error' => [
                'code' => $code,
                'message' => $message,
                'details' => (object) $details,
            ],
        ];
    }
}
