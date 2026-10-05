<?php

namespace App\Exceptions;

use RuntimeException;

class UnauthenticatedException extends RuntimeException
{
    public function __construct(
        string $message = 'Unauthenticated.',
    ) {
        parent::__construct($message);
    }
}
