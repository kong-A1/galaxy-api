<?php

use App\Http\Controllers\CMS\AuthController;
use App\Http\Controllers\CMS\UserController;
use Illuminate\Support\Facades\Route;

Route::prefix('auth')->group(function () {
    Route::post('/login', [AuthController::class, 'login']);
});

Route::prefix('user')->group(function () {
    Route::post('/create', [UserController::class, 'create']);
});
