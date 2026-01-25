FROM mcr.microsoft.com/devcontainers/dotnet:8.0

ENV DEBIAN_FRONTEND=noninteractive

RUN curl -sS https://dl.yarnpkg.com/debian/pubkey.gpg | apt-key add - \
    && echo "deb https://dl.yarnpkg.com/debian/ stable main" | tee /etc/apt/sources.list.d/yarn.list

RUN apt-get update \
        && apt-get -y install --no-install-recommends \
            libice6 \
            libsm6 \
        && apt-get -y autoremove \
        && apt-get -y clean \
        && rm -rf /var/lib/apt/lists/*

ENV DEBIAN_FRONTEND=dialog

ENV VNC_RESOLUTION=640x960x32
