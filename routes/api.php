<?php

use Illuminate\Support\Facades\Route;

Route::prefix('cms')->group(function () {
    require __DIR__.'/cms/user.php';
});

// Route::prefix('line')->group(function () {
//     require ;
// });
