# gateway-forward-auth

in this scenario, [traefik](https://doc.traefik.io/traefik/) is used as the gateway.

![alt text](docs/flow.png)

learned:
- caveats
    - authorization or role based access may be tricky
      - the auth server would need to store role/permission logic for every backend service.
      - the auth server could extract and forward roles from the jwt to downstream services through http headers. this may be unoptimal for systems with very granular roles.
