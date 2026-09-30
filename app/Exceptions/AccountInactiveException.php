<?php

namespace App\Exceptions;

use RuntimeException;

class AccountInactiveException extends RuntimeException
{
    public function __construct()
    {
        parent::__construct('ACCOUNT_INACTIVE');
    }
}
