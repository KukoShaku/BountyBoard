/// <binding BeforeBuild='default, karma-config' />
//IMPORTS
var gulp = require('gulp');
var concat = require('gulp-concat');
var uglify = require('gulp-uglify');
var minifyCSS = require('gulp-minify-css');
var sourcemaps = require('gulp-sourcemaps');
var del = require('del');
var rename = require('gulp-rename');
var changed = require('gulp-changed');
var resources = require('gulp-resources');
var inject = require('gulp-inject');
var filter = require('gulp-filter');

//CONFIG
var config = {
    input: {
        bootstrap: {
            scripts: ['bower_components/bootstrap/dist/js/bootstrap.js'],
            content: ['bower_components/bootstrap/dist/css/bootstrap.css'],
            fonts: ['bower_components/bootstrap/dist/fonts/**/*']
        },
        jquery: ['bower_components/jquery/dist/jquery.js'],
        angular: [
            'bower_components/angular/angular.js',
            'bower_components/angular-route/angular-route.js',
            'bower_components/angular-bootstrap/ui-bootstrap-tpls.js',
            'bower_components/angular-animate/angular-animate.js'
        ],
        underscore: ['bower_components/underscore/underscore.js'],
        deployment: ['WebFiles/**']
    },
    output: {
        defaultDeployment: '\\\\teamview.com@SSL\\demo\\testsite\\Bounty_Board',
        scripts: 'WebFiles/Scripts/lib',
        content: 'WebFiles/Content/lib',
        bootstrap: {
            scripts: 'bootstrap-bundle.min.js',
            content: 'bootstrap-bundle.min.css',
            fonts: 'WebFiles/Content/fonts'
        },
        jquery: 'jquery-bundle.min.js',
        angular: 'angular-bundle.min.js',
        underscore: 'underscore-bundle.min.js',
    },
    clean: {
        scripts: ['WebFiles/Scripts/lib/*'],
        content: ['WebFiles/Content/lib/*', 'content/fonts']
    }
};

//HELPERS
function genericMinifyAndBundle(cfg, transform, output) {
    if (typeof cfg.src === 'undefined') {
        throw "No source glob supplied for genericMinifyAndBundle()";
    }
    if (typeof cfg.filename === 'undefined') {
        throw "No output filename supplied for genericMinifyAndBundle()";
    }
    if (typeof transform === 'undefined') {
        throw "No transform function supplied for genericMinifyAndBundle()";
    }
    if (typeof output === 'undefined') {
        throw "No output directory supplied for genericMinifyAndBundle()";
    }

    return gulp.src(cfg.src)
        .pipe(concat(cfg.filename))
        .pipe(sourcemaps.init())
            .pipe(transform())
        .pipe(sourcemaps.write('./'))
        .pipe(gulp.dest(output));
}

function jsMinifyAndBundle(cfg) {
    return genericMinifyAndBundle(cfg, uglify, config.output.scripts);
}

function cssMinifyAndBundle(cfg) {
    return genericMinifyAndBundle(cfg, minifyCSS, config.output.content);
}


//TASKS
//Bootstrap
gulp.task("bootstrap-scripts", [], function bootstrapScripts() {
    return jsMinifyAndBundle({
        src: config.input.bootstrap.scripts,
        filename: config.output.bootstrap.scripts
    });
});

gulp.task("bootstrap-content", [], function bootstrapContent() {
    return cssMinifyAndBundle({
        src: config.input.bootstrap.content,
        filename: config.output.bootstrap.content
    });
});

gulp.task("bootstrap-fonts", [], function bootstrapFonts() {
    return gulp.src(config.input.bootstrap.fonts)
        .pipe(gulp.dest(config.output.bootstrap.fonts));
});

gulp.task("bootstrap", ["bootstrap-scripts", "bootstrap-content", "bootstrap-fonts"], function bootstrap() {
});

//jQuery
gulp.task("jquery", [], function jquery() {
    return jsMinifyAndBundle({
        src: config.input.jquery,
        filename: config.output.jquery
    });
});

//angular
gulp.task("angular", [], function angular() {
    return jsMinifyAndBundle({
        src: config.input.angular,
        filename: config.output.angular
    });
});

//underscore
gulp.task("underscore", [], function underscore() {
    return jsMinifyAndBundle({
        src: config.input.underscore,
        filename: config.output.underscore
    });
});


//Generic
gulp.task("scripts", ["jquery", "bootstrap-scripts", "angular", "underscore"], function scripts() {
});

gulp.task("content", ["bootstrap-content", "bootstrap-fonts"], function content() {
});

gulp.task("default", ["content", "scripts"], function _default() {
});

//Clean
gulp.task("clean-scripts", [], function cleanBootstrap() {
    return del(config.clean.scripts);
});

gulp.task("clean-content", [], function cleanBootstrap() {
    return del(config.clean.content);
});

gulp.task("clean-all", ["clean-scripts", "clean-content"], function clean() {
});

//Deploy
gulp.task('deploy', [], function () {
    var srcList = config.input.deployment;
    var dest = config.output.defaultDeployment;

    console.log("DEPLOYING");
    console.log("================");

    console.log("Sources: ");
    console.log(srcList);

    console.log("Writing to " + dest);
    console.log("If deployment to remote site fails, navigate to following address in Explorer and enter Security Credentials: ");
    console.log(dest);

    return gulp.src(srcList)
        //Only deploy files that have a more recent lastModifiedDate - AMAZING!
        .pipe(changed(dest))
        //Rename all HTMLs to .aspx (because SharePoint)
        //TODO only rename the top level HTML
        .pipe(rename(function (path) {
            console.log(path.dirname + '\\' + path.basename + path.extname);
            if (path.extname === '.html' && path.basename === 'MAINPAGENAME.html') {
                path.extname = '.aspx';
            }
        }))
        .pipe(gulp.dest(dest));
});

//Karma config
gulp.task('karma-config', [], function () {
    //Read all JS resources listed in index.html
    var sources = gulp.src('WebFiles/index.html')
        .pipe(resources({
            css: false,
            less: false
        }))
        .pipe(filter(['**/*.js', '!index.html']));


    //Inject these names into karma.conf.js under '/* inject:files */' 
    return gulp.src('karma.conf.js')
        .pipe(inject(sources, {
            starttag: '/* inject:files */',
            endtag: '/* endinject */',
            transform: function (filepath, file, i, length) {
                return '"' + process.cwd().replace(/\\/g, '/') + filepath + '",';
            }
        }))
        .pipe(gulp.dest('./'));
});
