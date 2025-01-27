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

kubectl get services
-> This will get the list of services

kubectl get pods
-> This will give the list of pods running

kubectl delete deployment deploymentname
-> This will delete the deployment whose name is deploymentname

kubectl rollout restart deployment deploymentname
-> This will restart the pods by taking the image from the docker hub

kubectl create secret generic mssql --from-literal=SA_PASSWORD="somepassword"
->This will create a generic secret in kubernetes whose key is "SA_PASSWORD" and value is "somepassword"

# rabbit mq
Rabbit Mq is used to communicate between microservices asynchronously. It has different types of exchanges like Direct, FanOut, Topic & Header.
-> Direct Exchange delivers messages to queues based on an exact match between the message's routing key and the queue's binding key.
-> Fanout Exchange fanout exchange broadcasts messages to all queues bound to it, ignoring the routing key.
-> Topic Exchange routes messages to queues based on a pattern match between the routing key and a queue's binding pattern, using wildcards (* for one word and # for multiple words).
-> Headers Exchange routes messages based on header attributes instead of the routing key, requiring an exact match between the message's headers and the binding's header conditions.

# GRPC
gRPC (Google Remote Procedure Call) is one way to communicate between microservices synchronously. It is faster than HTTP because it uses Protobuf (a binary format) instead of JSON.
