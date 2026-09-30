<?php

namespace App\Exceptions;

use RuntimeException;

class EmailAlreadyExistsException extends RuntimeException
{
    public function __construct()
    {
        parent::__construct('EMAIL_ALREADY_EXISTS');
    }
}
