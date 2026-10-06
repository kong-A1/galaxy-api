<?php

namespace App\OpenApi\Helpers;

use OpenApi\Attributes as OA;

#[OA\Info(
    version: '1.0.0',
    title: 'Galaxy API',
    description: 'Galaxy API Documentation',
)]

#[OA\Server(
    url: 'http://127.0.0.1:8000',
    description: 'Local development server',
)]

#[OA\Tag(
    name: 'Galaxy CMS API',
    description: 'CMS management APIs',
)]
class OpenApiInfo {}
