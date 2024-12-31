# docker commands :

docker build -t GSGPavan/PlatformService .
-> This builds the Dockerfile and tags the image with "PlatformService" but the format is dockerdesktopusername/nameOfImage

docker run -p 8080:80 -d GSGPavan/PlatformService
-> This runs the image "PlatformService" but the format is dockerdesktopusername/nameOfImage
-p stands for port mapping. 8080 is external port i.e hosts port and 80 is internal port i.e containers port
-d stands for detached mode so that we can work on the same terminal for other commands
Everytime this command is executed, a new running container of this image is created

docker ps
-> This gives the list of containers that are currently running. It gives the container id , image name etc..

docker stop containerId
-> This stops the container whose id is "containerId".

docker start containerId
-> This starts the container whose id is "containerId".

docker push GSGPavan/PlatformService
-> This pushes the PlatformService image to the dockerhub whose account is "GSGPavan"


# K8S commands :

kubectl apply -f PlatformService-depl.yaml
-> This creates the deployment. Creating the pods and running the containers with the specified image

kubectl get deployments
-> This will give the list of deployments

kubectl get pods
-> This will give the list of pods running

kubectl delete deployment deploymentname
-> This will delete the deployment whose name is deploymentname