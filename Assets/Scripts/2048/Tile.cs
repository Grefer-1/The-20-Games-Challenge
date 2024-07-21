using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public TileState State {  get; private set; }
    public TileCell Cell { get; private set; }
    public int Value { get; private set; }
    public bool Locked { get; private set; }

    private Image Background;
    private TextMeshProUGUI Text;

    void Awake()
    {
        Background = GetComponent<Image>();
        Text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Unlock() => Locked = false;

    public void SetState(TileState state, int value)
    {
        State = state;
        Value = value;
        Background.color = state.Background;
        Text.color = state.Text;
        Text.text = Value.ToString();
    }

    public void Spawn(TileCell cell)
    {
        if (Cell != null)
            Cell.Tile = null;

        Cell = cell;
        Cell.Tile = this;
        transform.position = cell.transform.position;
    }

    public void MoveTo(TileCell cell)
    {
        StartCoroutine(Animate(cell.transform.position));
    }

    public void Merge(TileCell cell)
    {
        if (Cell != null)
            Cell.Tile = null;

        Cell.Tile = null;
        Cell.Tile.Locked = true;

        StartCoroutine(Animate(cell.transform.position, true));
    }

    private IEnumerator Animate(Vector3 target, bool merge = false)
    {
        float elapsed = 0f;
        float duration = 0.1f;

        Vector3 current = transform.position;

        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(current, target, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = target;

        if (merge)
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
