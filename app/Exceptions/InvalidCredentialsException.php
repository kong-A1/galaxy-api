<?php

namespace App\Exceptions;

use Illuminate\Auth\AuthenticationException;

class InvalidCredentialsException extends AuthenticationException
{
    public function __construct()
    {
        parent::__construct('The provided credentials are incorrect.');
    }
}
