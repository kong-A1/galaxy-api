<?php

namespace Tests\Feature\Http\Requests\CMS;

use App\Http\Requests\CMS\LoginRequest;
use Illuminate\Support\Facades\Validator;
use Tests\TestCase;

class LoginRequestTest extends TestCase
{
    public function test_it_passes_with_valid_data(): void
    {
        $request = new LoginRequest;

        $validator = Validator::make(
            [
                'email' => 'john.doe@example.com',
                'password' => 'pass@123',
            ],
            $request->rules(),
        );

        $this->assertFalse($validator->fails());
    }

    public function test_it_requires_email(): void
    {
        $request = new LoginRequest;

        $validator = Validator::make(
            [
                'password' => 'pass@123',
            ],
            $request->rules(),
        );

        $this->assertTrue($validator->fails());
        $this->assertArrayHasKey('email', $validator->errors()->toArray());
    }

    public function test_it_requires_valid_email(): void
    {
        $request = new LoginRequest;

        $validator = Validator::make(
            [
                'email' => 'invalid-email',
                'password' => 'pass@123',
            ],
            $request->rules(),
        );

        $this->assertTrue($validator->fails());
        $this->assertArrayHasKey('email', $validator->errors()->toArray());
    }

    public function test_it_requires_password(): void
    {
        $request = new LoginRequest;

        $validator = Validator::make(
            [
                'email' => 'john.doe@example.com',
            ],
            $request->rules(),
        );

        $this->assertTrue($validator->fails());
        $this->assertArrayHasKey('password', $validator->errors()->toArray());
    }
}
