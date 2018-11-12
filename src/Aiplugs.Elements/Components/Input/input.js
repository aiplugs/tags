AiplugsElements.register("aiplugs-input", class extends Stimulus.Controller {
  static get targets() {
    return ["input", "suggestion"];
  }
  search() {
    const url = this.ajaxUrl.replace("{0}", this.inputTarget.value);
    const opts = {
      method: "GET",
      headers: this.ajaxHeaders,
      mode: "cors",
      credentials: "include"
    }
    return fetch(url, opts).then(res => {
      if (res.ok)
        return res.json();
    }).then(data => Array.isArray(data) ? data : []);
  }
  onInput() {
    this.debounce('oninput', 300, () => {
      if (this.ajaxUrl) {
        if (this.unique)
          this.check();
        else
          this.suggestion();
      }
    });
  }
  onBlur() {
    if (this.ajaxUrl && !this.unique) {
      const value = this.inputTarget.value;
      const valueKey = this.ajaxValue;
      const rule = this.ignoreCase ? datum => (datum[valueKey] || "").toLowerCase() === value
        : datum => (datum[valueKey] || "") === value
      this.search().then(data => {
        const datum = data.find(rule) || data.find(datum => (datum[valueKey] || "").startsWith(value)) || data[0];
        if (datum) {
          this.inputTarget.value = datum[valueKey];
        }
      })
    }
  }
  check() {
    const value = this.inputTarget.value;
    const valueKey = this.ajaxValue;
    const rule = this.ignoreCase ? datum => (datum[valueKey] || "").toLowerCase() === value
      : datum => (datum[valueKey] || "") === value
    this.search().then(data => {
      const duplicated = !!data.find(rule);
      this.element.classList.toggle("aiplugs-input--duplicated", duplicated);
      this.element.classList.toggle("aiplugs-input--unique", !duplicated);
    })
  }
  suggestion() {
    const labelKey = this.ajaxLabel;
    const valueKey = this.ajaxValue;
    this.search().then(data => {
      this.suggestionTarget.innerHTML = "";
      data.forEach(datum => {
        const option = document.createElement("option");
        option.innerText = datum[labelKey];
        option.label = datum[labelKey];
        option.value = datum[valueKey];
        this.suggestionTarget.appendChild(option);
      });
    });
  }
  get unique() {
    return this.data.get("unique") === "true";
  }
  get ignoreCase() {
    return this.data.get("ignore-case") === "true";
  }
  get ajaxUrl() {
    return this.data.get("ajax-url");
  }
  get ajaxHeaders() {
    const prefix = "data-aiplugs-input-ajax-headers-";
    return Array.from(this.element.attributes)
      .filter(attr => attr.name.startsWith(prefix))
      .reduce((headers, attr) => {
        headers[attr.name.replace(prefix, "")] = attr.value;
      }, {});
  }
  get ajaxLabel() {
    return this.data.get("ajax-label") || "label";
  }
  get ajaxValue() {
    return this.data.get("ajax-value") || "value";
  }
});