<template>
  <div class="slidev-layout">
    <div>
        <slot name="top" />
    </div>
    <div class="two-columns w-full grid grid-cols-2">
      <div class="col-left">
        <slot name="left" />
      </div>
      <div class="col-right" style="margin-left:1rem;">
        <slot name="right" />
      </div>
    </div>
    <div>
        <slot name="bottom" />
    </div>
  </div>
</template>