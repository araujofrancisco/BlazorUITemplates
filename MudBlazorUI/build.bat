docker build -t mudblazorui . 
docker image prune --force --filter label=stage=build-env
docker image prune --force --filter dangling=true
docker save mudblazorui > mudblazorui.tar