# gateway-auth

In this scenario, [traefik](https://doc.traefik.io/traefik/) is the gateway, and we are using it's 'ForwardAuth' feature.

![alt text](docs/flow.png)

learned...
- Caveats
    - This approach is tricky for systems with very granular authorization requirements.
      - Considerations
        1. The auth server could store role/access logic for every backend service & endpoint.
        2. The auth server could extract and forward the user context from the jwt to downstream services through http headers. (*approach taken here*)

- Points of interest
    - Services don't need to be aware of authentication.
    - Service-to-service communication is simple. Just pass the user context around, which is hopefully fine in a trusted environment.

Alternative:
- Scrap gateway auth and let each service handle its own authentication logic.

## example

Example response from `POST /items`. 

```json
{
    "itemId": "c7140ef4-de15-4f5e-b804-77fda9ac62e5",
    "userInfo": {
        "id": "a5a393fb-c73e-47e1-8da7-73b4824e4b8d",
        "roles": "supporter pioneer"
    }
}
```

The user context was forwarded to the item-service through http headers: `X-User-Id` and `X-User-Roles`. We can also use this info to deny/allow access on a per-service basis.

