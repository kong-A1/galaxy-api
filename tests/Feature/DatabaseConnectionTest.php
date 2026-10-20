<?php

namespace Tests\Feature;

use Illuminate\Support\Facades\DB;
use Tests\TestCase;

class DatabaseConnectionTest extends TestCase
{
    public function test_it_uses_the_postgresql_test_database(): void
    {
        $connection = DB::connection();

        $this->assertSame('pgsql', $connection->getDriverName());
        $this->assertSame('galaxy_test', $connection->getDatabaseName());
        $this->assertSame(1, (int) DB::selectOne('select 1 as ok')->ok);
    }
}
