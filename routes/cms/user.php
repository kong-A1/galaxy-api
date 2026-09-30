<?php

use App\Http\Controllers\CMS\UserController;
use Illuminate\Support\Facades\Route;

Route::prefix('user')->group(function () {
    Route::post('/create', [UserController::class, 'create']);
});
