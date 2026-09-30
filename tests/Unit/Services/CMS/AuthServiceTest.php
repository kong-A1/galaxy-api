<?php

namespace Tests\Unit\Services\CMS;

use App\Exceptions\AccountInactiveException;
use App\Exceptions\InvalidCredentialsException;
use App\Models\User;
use App\Queries\CMS\UserQuery;
use App\Services\CMS\AuthService;
use Illuminate\Support\Facades\Auth;
use Illuminate\Support\Facades\Hash;
use Tests\TestCase;

class AuthServiceTest extends TestCase
{
    public function test_it_logs_in_successfully(): void
    {
        $user = new User([
            'name' => 'John Doe',
            'email' => 'john.doe@example.com',
            'status' => 'active',
        ]);

        $user->setRawAttributes([
            ...$user->getAttributes(),
            'password' => 'hashed-password',
        ], true);

        $userQuery = $this->createMock(UserQuery::class);

        $userQuery
            ->expects($this->once())
            ->method('findByEmail')
            ->with('john.doe@example.com')
            ->willReturn($user);

        Hash::shouldReceive('check')
            ->once()
            ->with('password123', 'hashed-password')
            ->andReturnTrue();

        Auth::shouldReceive('login')
            ->once()
            ->with($user);

        $service = new AuthService($userQuery);

        $result = $service->login(
            'john.doe@example.com',
            'password123',
        );

        $this->assertSame($user, $result);
    }

    public function test_it_throws_exception_when_user_does_not_exist(): void
    {
        $userQuery = $this->createMock(UserQuery::class);

        $userQuery
            ->expects($this->once())
            ->method('findByEmail')
            ->with('not-found@example.com')
            ->willReturn(null);

        Hash::shouldReceive('check')->never();
        Auth::shouldReceive('login')->never();

        $service = new AuthService($userQuery);

        $this->expectException(InvalidCredentialsException::class);

        $service->login(
            'not-found@example.com',
            'password123',
        );
    }

    public function test_it_throws_exception_when_password_is_invalid(): void
    {
        $user = new User([
            'name' => 'John Doe',
            'email' => 'john.doe@example.com',
            'status' => 'active',
        ]);

        $user->setRawAttributes([
            'password' => 'hashed-password',
        ], true);

        $userQuery = $this->createMock(UserQuery::class);

        $userQuery
            ->expects($this->once())
            ->method('findByEmail')
            ->with('john.doe@example.com')
            ->willReturn($user);

        Hash::shouldReceive('check')
            ->once()
            ->with('wrong-password', 'hashed-password')
            ->andReturnFalse();

        Auth::shouldReceive('login')->never();

        $service = new AuthService($userQuery);

        $this->expectException(InvalidCredentialsException::class);

        $service->login(
            'john.doe@example.com',
            'wrong-password',
        );
    }

    public function test_it_throws_exception_when_account_is_inactive(): void
    {
        $user = new User([
            'name' => 'John Doe',
            'email' => 'john.doe@example.com',
            'status' => 'inactive',
        ]);

        $user->setRawAttributes([
            'password' => 'hashed-password',
        ], true);

        $userQuery = $this->createMock(UserQuery::class);

        $userQuery
            ->expects($this->once())
            ->method('findByEmail')
            ->with('john.doe@example.com')
            ->willReturn($user);

        Hash::shouldReceive('check')
            ->once()
            ->with('password123', 'hashed-password')
            ->andReturnTrue();

        Auth::shouldReceive('login')->never();

        $service = new AuthService($userQuery);

        $this->expectException(AccountInactiveException::class);

        $service->login(
            'john.doe@example.com',
            'password123',
        );
    }
}
