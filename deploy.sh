#!/bin/sh

ssh -p 2425 root@10.0.2.15 /bin/bash <<EOF 
  sudo echo "New value"
  cd backend_project
  cd CodeRouteAPI
  git add .
  git commit -m "Local changes"
  git pull origin main
  cp -r Server/* /var/www/test_backend_app/CodeRoute/Server/
  cd /etc/systemd/system
  sudo systemctl restart kestrel-deploy-guide.service
  exit
EOF
