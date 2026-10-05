<?php

use App\Http\Controllers\CMS\AuthController;
use App\Http\Controllers\CMS\UserController;
use Illuminate\Support\Facades\Route;

Route::prefix('auth')->group(function () {
    Route::post('/login', [AuthController::class, 'login']);

    Route::middleware('auth:sanctum')->group(function () {
        Route::get('/me', [AuthController::class, 'me']);
        Route::post('/logout', [AuthController::class, 'logout']);
    });
});

Route::prefix('user')->middleware('auth:sanctum')->group(function () {
    Route::post('/create', [UserController::class, 'create']);
});
