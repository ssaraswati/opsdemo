# Kubernetes OPS demo chart
shows example's of grafana with prometheus and the EFK stack using fluent-bit as well as chaoskube
bundles an ngnix ingress to allow access, default values file works well with docker for desktop  installing in other systems will need some configuration


helm upgrade --install opsdemo ./ opsdemo

Kibana filter to find example structured logs from the noisylogs image
```
kubernetes.labels.app="opsdemo-noisylogs"
```