<?php

namespace Tests\Feature\tests\Unit\Services\CMS;

use App\Exceptions\EmailAlreadyExistsException;
use App\Queries\CMS\UserQuery;
use App\Services\CMS\UserService;
use Illuminate\Database\QueryException;
use Tests\TestCase;

class UserServiceTest extends TestCase
{
    /**
     * Test create user failed when email already exists
     */
    public function test_it_converts_email_unique_violation_to_email_already_exists_exception(): void
    {
        $userQuery = $this->createMock(UserQuery::class);

        $userQuery
            ->expects($this->once())
            ->method('existsByEmail')
            ->with('john.doe@example.com')
            ->willReturn(false);

        $userQuery
            ->expects($this->once())
            ->method('create')
            ->willThrowException(
                new QueryException(
                    'pgsql',
                    'insert into "users" (...) values (...)',
                    [],
                    new \PDOException(
                        'SQLSTATE[23505]: Unique violation: 7 ERROR: duplicate key value violates unique constraint "users_email_unique"',
                        23505,
                    ),
                ),
            );

        $service = new UserService($userQuery);

        $this->expectException(EmailAlreadyExistsException::class);

        $service->create([
            'name' => 'John Doe',
            'email' => 'john.doe@example.com',
            'password' => 'password123',
        ]);
    }
}
