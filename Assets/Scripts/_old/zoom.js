    #pragma strict
     
    var camPos : Vector3;
    var camTr : Transform;
    var speed = 2.5;
     
    function Start() {
    camTr = Camera.main.transform;
    camPos = camTr.position;
    }
     
    function Update() {
     
    if (Input.GetMouseButtonDown(0)) {
    var hit : RaycastHit;
    var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    if (Physics.Raycast(ray, hit) && hit.collider.tag == "MSOE") {
    var blocks = GameObject.FindGameObjectsWithTag("MSOE");
    for (var go : GameObject in blocks) {
    if (go == hit.collider.gameObject) {
    camPos.x = go.transform.position.x;
    camPos.y = go.transform.position.y;
    camPos.z += 5.0; // Zoom
    }
    else {
    //go.SetActive(false);
     
    }
    }
    }
    }
     
    camTr.position = Vector3.Lerp(camTr.position, camPos, Time.deltaTime * speed);
    }