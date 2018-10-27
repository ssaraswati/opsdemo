FROM gcr.io/google-containers/fluentd-elasticsearch:v2.3.1

RUN clean-install $BUILD_DEPS \
                     python \
                     python-requests \
    && apt-get purge -y --auto-remove \
                     -o APT::AutoRemove::RecommendsImportant=false \
                     $BUILD_DEPS \