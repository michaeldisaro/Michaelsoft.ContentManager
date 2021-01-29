'use strict';

let gulp = require('gulp'),
    babel = require('gulp-babel'),
    concat = require('gulp-concat'),
    cssmin = require('gulp-cssmin'),
    uglify = require('gulp-uglify-es').default,
    del = require('del'),
    bundleconfig = require('./bundleconfig.json');

gulp.task("clean", async function () {
    del(bundleconfig.map(bundle => bundle.outputFileName));
});

gulp.task("min", async function () {
    bundleconfig.forEach(bundle => {
        switch (bundle.type) {
            case "css":
                minifyCss(bundle.inputFiles, bundle.outputFileName);
                break;
            case"js":
                minifyJs(bundle.inputFiles, bundle.outputFileName);
                break;
            default:
                break;
        }
    });
});

gulp.task("default", gulp.series(["clean", "min"]));

async function minifyCss(inputFiles, outputFileName) {
    return gulp.src(inputFiles, {base: '.'})
        .pipe(concat(outputFileName))
        .pipe(cssmin())
        .pipe(gulp.dest('.'));
}

async function minifyJs(inputFiles, outputFileName) {
    return gulp.src(inputFiles, {base: '.'})
        .pipe(babel({presets: ['@babel/preset-env']}))
        .pipe(concat(outputFileName))
        .pipe(uglify().on('error', function (e) {
            console.log(e);
        }))
        .pipe(gulp.dest('.'));
}